using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Simpl.Fundamental.Collections;
using Simpl.Fundamental.Generic;
using Simpl.Fundamental.PlatformSpecifics;
using Simpl.Serialization;

namespace Ecologylab.Semantics.MetaMetadataNS
{
    public class MetaMetadataRepositoryLoader
    {
         /**
	     * registry of formats to file name extensions.
	     */
	    private static readonly Dictionary<Format, String>	fileNameExts	= new Dictionary<Format, string>();

	    static MetaMetadataRepositoryLoader()
	    {
		    fileNameExts.Put(Format.Xml, ".xml");
		    fileNameExts.Put(Format.Json, ".json");
	    }

        public static async Task<MetaMetadataRepository> ReadDirectoryRecursively(MetaMetadataRepository mainRepo, String path, SimplTypesScope mmdTScope, SimplTypesScope metadataTScope)
        {
            Stack<string> stack = new Stack<string>();
            stack.Push(path);
            while(stack.Count > 0)
            {
                string dir = stack.Pop();
                Debug.WriteLine("Looking in : " + dir);
                //String[] files = Directory.GetFiles(dir, "*.xml");
                string[] files = await FundamentalPlatformSpecifics.Get().GetFilesFromDirectory(dir, ".xml");
                foreach (string file in files)
                {
                    MetaMetadataRepository thatRepo = await ReadRepository(file, mmdTScope, metadataTScope, mainRepo);
                    if (thatRepo != null)
                        mainRepo.IntegrateRepository(thatRepo);

                    //string[] directories = Directory.GetDirectories(dir);
                    //                                           string[] directories = await FundamentalPlatformSpecifics.Get().GetDirectoriesFromDirectory(dir);
                    //                                           foreach (
                    //                                                    string innerDir in directories.Where(
                    //                                                                                          innerDir =>
                    //                                                                                            !innerDir.Contains(".svn")))
                    //                                                                                    stack.Push(innerDir);
                    //                                                                            });
                }
                
            }

            return mainRepo;
        }

        public static async Task<MetaMetadataRepository> ReadRepository(string filename, SimplTypesScope mmdTScope, SimplTypesScope metadataTScope, MetaMetadataRepository mainRepo)
        {
            MetaMetadataRepository repo = null;
            Debug.WriteLine("MetaMetadataRepository Reading:\t\t" + filename);

            try
            {
                repo = await mmdTScope.DeserializeFile(filename, Format.Xml) as MetaMetadataRepository;
                if (repo != null)
                {
                    repo.MetadataTScope = metadataTScope;
                    repo.File = filename;
                    repo.InitializeSuffixAndMimeDicts();

                    if (repo.RepositoryByName == null)
                        return repo;

                    foreach (var repoEntry in repo.RepositoryByName)
                    {
                        MetaMetadata mmd = repoEntry.Value;
                        string mmdName = repoEntry.Key;

                        //mmd.File = new FileInfo(filename);
                        mmd.File = await FundamentalPlatformSpecifics.Get().CreateFile(filename);
                        mmd.Parent = mainRepo;
                        mmd.Repository = mainRepo;

                        string packageName = mmd.PackageName ?? repo.PackageName;
                        if (packageName == null)
                            throw new MetaMetadataException("No Package Name Specified For " + mmd);
                        mmd.PackageName = packageName;

                        MmdScope packageMmdScopes;
                        mainRepo.PackageMmdScopes.TryGetValue(mmd.PackageName, out packageMmdScopes);
                        if(packageMmdScopes == null)
                        {
                            packageMmdScopes = new MmdScope(mainRepo.RepositoryByName);
                            mainRepo.PackageMmdScopes.Put(packageName, packageMmdScopes);
                        }

                        MetaMetadata existingMmd;
                        switch (mmd.Visibility)
                        {
                            case Visibility.GLOBAL:
                            
                                mainRepo.RepositoryByName.TryGetValue(mmdName, out existingMmd);

                                if (existingMmd != null && existingMmd != mmd)
                                    throw new MetaMetadataException("MMD already exists: " + mmdName + " in " + filename);

                                mainRepo.RepositoryByName.Put(mmdName, mmd);
                                break;
                            case Visibility.PACKAGE:
                                packageMmdScopes.TryGetValue(mmdName, out existingMmd);

                                if (existingMmd != null && existingMmd != mmd)
                                    throw new MetaMetadataException("MMD already exists: " + mmdName + " in " + filename);

                                packageMmdScopes.Put(mmdName, mmd);
                                break;
                        }
                    }

                    foreach (MetaMetadata metaMetadata in repo.RepositoryByName.Values)
                    {
                        if (metaMetadata.PackageName == null)
                        {
                            Debug.WriteLine("No Package name defined for: " + metaMetadata.Name);
                            continue;
                        }
                        MmdScope packageMmdScope;
                        mainRepo.PackageMmdScopes.TryGetValue(metaMetadata.PackageName, out packageMmdScope);
                        metaMetadata.MmdScope = packageMmdScope;
                    }

                    mainRepo.IntegrateRepository(repo);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Couldn't translate repository file: " + filename);
                Debug.WriteLine(e);
            }

            return repo;
        }

    }
}