using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Simpl.Fundamental.Collections;
using Simpl.Fundamental.Generic;
using Simpl.Serialization;

namespace ecologylab.semantics.metametadata
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

        public static MetaMetadataRepository ReadDirectoryRecursively(String path, SimplTypesScope mmdTScope, SimplTypesScope metadataTScope)
        {
            MetaMetadataRepository mainRepo = new MetaMetadataRepository
                {
                    RepositoryByName = new Dictionary<string, MetaMetadata>(),
                    PackageMmdScopes = new Dictionary<string, MultiAncestorScope<MetaMetadata>>()
                };

            Stack<string> stack = new Stack<string>();
            stack.Push(path);
            while(stack.Count > 0)
            {
                string dir = stack.Pop();
                Console.WriteLine("Looking in : "  + dir);
                String[] files = Directory.GetFiles(dir, "*.xml");
                foreach (String file in files)
                {
                    MetaMetadataRepository thatRepo = ReadRepository(file, mmdTScope, metadataTScope, mainRepo);
                    mainRepo.IntegrateRepository(thatRepo);
                }
                string[] directories = Directory.GetDirectories(dir);
                foreach (string innerDir in directories.Where(innerDir => !innerDir.Contains(".svn")))
                    stack.Push(innerDir);
            }

            mainRepo.BindMetadataClassDescriptorsToMetaMetadata(metadataTScope);

            return mainRepo;
        }

        public static MetaMetadataRepository ReadRepository(String filename, SimplTypesScope mmdTScope, SimplTypesScope metadataTScope, MetaMetadataRepository mainRepo)
        {
            MetaMetadataRepository repo = null;
            //Console.WriteLine("MetaMetadataRepository Reading: " + filename);

            try
            {
                repo = (MetaMetadataRepository)mmdTScope.DeserializeFile(filename, StringFormat.Xml);
                repo.MetadataTScope = metadataTScope;
                repo.File = filename;
                repo.InitializeSuffixAndMimeDicts();

                if (repo.RepositoryByName == null)
                    return repo;

                foreach (var repoEntry in repo.RepositoryByName)
                {
                    MetaMetadata mmd = repoEntry.Value;
                    string mmdName = repoEntry.Key;

                    mmd.File = new FileInfo(filename);
                    mmd.Parent = mainRepo;
                    mmd.Repository = mainRepo;

                    string packageName = mmd.PackageName ?? repo.PackageName;
                    if (packageName == null)
                        throw new MetaMetadataException("No Package Name Specified For " + mmd);
                    mmd.PackageName = packageName;

                    MultiAncestorScope<MetaMetadata> packageMmdScopes;
                    mainRepo.PackageMmdScopes.TryGetValue(mmd.PackageName, out packageMmdScopes);
                    if(packageMmdScopes == null)
                    {
                        packageMmdScopes = new MultiAncestorScope<MetaMetadata>(new[] {mainRepo.RepositoryByName});
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

                    foreach (MetaMetadata metaMetadata in repo.RepositoryByName.Values)
                    {
                        if (metaMetadata.PackageName == null)
                        {
                            Debug.WriteLine("No Package name defined for: " + metaMetadata.Name);
                            continue;
                        }
                        MultiAncestorScope<MetaMetadata> packageMmdScope;
                        mainRepo.PackageMmdScopes.TryGetValue(metaMetadata.PackageName, out packageMmdScope);
                        metaMetadata.MmdScope = packageMmdScope;
                    }

                    mainRepo.IntegrateRepository(repo);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't translate repository file: " + filename);
                Console.WriteLine(e);
            }

            return repo;
        }

    }
}