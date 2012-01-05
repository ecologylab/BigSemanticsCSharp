using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecologylab.semantics.metadata.builtins;
using Simpl.Serialization;

namespace MVVMTemplate
{
    class DocumentViewModel
    {
        public List<FieldViewModel> FieldViewModels { get; set; }

        public DocumentViewModel(Document parsedDoc)
        {
            FieldViewModels = new List<FieldViewModel>();

            foreach (FieldDescriptor descriptor in parsedDoc.ClassDescriptor.GetAllFields())
            {
                FieldViewModel fieldViewModel = new FieldViewModel(descriptor);
                FieldViewModels.Add(fieldViewModel);
            }
        }
    }
}
