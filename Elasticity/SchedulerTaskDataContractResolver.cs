using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

using Elasticity.Domain;

namespace Elasticity
{
    public class SchedulerTaskDataContractResolver : DataContractResolver
    {
        private Dictionary<string, Type> typesByNamespace = null;
        private Dictionary<Type, string> typesByType = null;

        public SchedulerTaskDataContractResolver()
        {
            typesByNamespace = new Dictionary<string, Type>();
            typesByType = new Dictionary<Type, string>();
            ConstructTypeMap();
        }

        private void ConstructTypeMap()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                try
                {
                    var availableTypes = from t in assembly.GetTypes()
                                         where t.GetCustomAttributes(typeof(SchedulerTaskContentAttribute), true).Count() > 0
                                         select t;

                    foreach (Type type in availableTypes)
                    {
                        object[] attributes = type.GetCustomAttributes(typeof(SchedulerTaskContentAttribute), true);
                        if (attributes.Length > 0)
                        {
                            SchedulerTaskContentAttribute taskContent = attributes[0] as SchedulerTaskContentAttribute;
                            string ns = taskContent.TypeNamespace + type.Name;

                            typesByNamespace.Add(ns, type);
                            typesByType.Add(type, ns);
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }

        }

        // Serialization
        public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out System.Xml.XmlDictionaryString typeName, out System.Xml.XmlDictionaryString typeNamespace)
        {
            Type[] genericTypes = type.GetGenericArguments();

            string ns = string.Empty;
            Type genericType = null;

            foreach (Type genType in genericTypes)
            {
                if (typesByType.ContainsKey(genType) == true)
                {
                    typesByType.TryGetValue(genType, out ns);
                    genericType = genType;
                    break;
                }
            }

            if (string.IsNullOrEmpty(ns) == false)
            {
                XmlDictionary dictionary = new XmlDictionary();
                typeName = dictionary.Add(genericType.Name);
                typeNamespace = dictionary.Add(ns);

                return true;
            }
            else
            {
                return knownTypeResolver.TryResolveType(type, declaredType, null, out typeName, out typeNamespace);
            }            
        }

        // Deserialization
        public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
        {
            Type type = null;
            typesByNamespace.TryGetValue(typeNamespace, out type);

            if (type != null)
            {
                Type genType = typeof(SchedulerTask<>);
                Type genericType = genType.MakeGenericType(type);
                return genericType;
            }
            else
            {
                // Defer to the known type resolver
                return knownTypeResolver.ResolveName(typeName, typeNamespace, null, knownTypeResolver);
            }
        }
    }
}
