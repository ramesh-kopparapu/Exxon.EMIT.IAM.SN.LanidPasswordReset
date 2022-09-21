using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace func_invitationtracking_acc.Helper
{
    class ResourceHelper
    {
        public static string ReturnTemplate(string templateName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $@"func_snpasswordreset_kamal.Templates.{templateName}";
            string resource = null;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    resource = reader.ReadToEnd();
                }
            }
            return resource;
        }
    }
}
