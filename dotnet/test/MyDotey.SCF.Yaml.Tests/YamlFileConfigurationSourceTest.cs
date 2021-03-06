using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

using MyDotey.SCF.Facade;

namespace MyDotey.SCF.Yaml
{
    /**
     * @author koqizhao
     *
     * Jul 20, 2018
     */
    public class YamlFileConfigurationSourceTest
    {
        [Fact]
        public virtual void TestDemo()
        {
            // create a scf yaml configuration source
            YamlFileConfigurationSourceConfig sourceConfig = new YamlFileConfigurationSourceConfig.Builder()
                    .SetName("yaml-file").SetFileName("test.yaml").Build();
            YamlFileConfigurationSource source = new YamlFileConfigurationSource(sourceConfig);

            // create scf manager & properties facade tool
            ConfigurationManagerConfig managerConfig = ConfigurationManagers.NewConfigBuilder().SetName("my-app")
                    .AddSource(1, source).Build();
            IConfigurationManager manager = ConfigurationManagers.NewManager(managerConfig);

            PropertyConfig<string, bool?> propertyConfig1 = ConfigurationProperties.NewConfigBuilder<string, bool?>()
                     .SetKey("booleanProperty").SetDefaultValue(false).Build();
            bool? boolValue = manager.GetPropertyValue(propertyConfig1);
            Console.WriteLine(boolValue);

            PropertyConfig<string, int?> propertyConfig2 = ConfigurationProperties.NewConfigBuilder<string, int?>()
                        .SetKey("intProperty").SetDefaultValue(0).Build();
            int? intValue = manager.GetPropertyValue(propertyConfig2);
            Console.WriteLine(intValue);

            PropertyConfig<string, long?> propertyConfig3 = ConfigurationProperties.NewConfigBuilder<string, long?>()
                        .SetKey("longProperty").SetDefaultValue(0L).Build();
            long? longValue = manager.GetPropertyValue(propertyConfig3);
            Console.WriteLine(longValue);

            PropertyConfig<string, string> propertyConfig4 = ConfigurationProperties.NewConfigBuilder<string, string>()
                        .SetKey("stringProperty").Build();
            string stringValue = manager.GetPropertyValue(propertyConfig4);
            Console.WriteLine(stringValue);

            PropertyConfig<string, List<string>> propertyConfig5 = ConfigurationProperties
                    .NewConfigBuilder<string, List<string>>().SetKey("listProperty").Build();
            List<string> listValue = manager.GetPropertyValue(propertyConfig5);
            Console.WriteLine(listValue == null ? null : string.Join(", ", listValue));

            PropertyConfig<string, Dictionary<string, string>> propertyConfig6 = ConfigurationProperties
                    .NewConfigBuilder<string, Dictionary<string, string>>().SetKey("mapProperty").Build();
            Dictionary<string, string> mapValue = manager.GetPropertyValue(propertyConfig6);
            Console.WriteLine(mapValue == null ? null :
                (string.Join(", ", mapValue.Select(p => string.Format("{0}: {1}", p.Key, p.Value)).ToList())));

            PropertyConfig<string, TestPojo> propertyConfig7 = ConfigurationProperties.NewConfigBuilder<string, TestPojo>()
                        .SetKey("objProperty").Build();
            TestPojo objValue = manager.GetPropertyValue(propertyConfig7);
            Console.WriteLine(objValue);
        }

        [Fact]
        public virtual void TestSequenceFile()
        {
            YamlFileConfigurationSourceConfig sourceConfig = new YamlFileConfigurationSourceConfig.Builder()
                    .SetName("yaml-file").SetFileName("bad-sequence-test.yaml").Build();
            new YamlFileConfigurationSource(sourceConfig);
        }
    }
}