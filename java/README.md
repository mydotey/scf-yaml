# SCF YAML Configuration Source

## maven dependency

```xml
    <dependencyManagement>
        <dependencies>
            <dependency>
                <groupId>org.mydotey.scf</groupId>
                <artifactId>scf-bom</artifactId>
                <version>1.5.3</version>
                <type>pom</type>
                <scope>import</scope>
            </dependency>
        </dependencies>
    </dependencyManagement>

    <dependencies>
        <dependency>
            <groupId>org.mydotey.scf</groupId>
            <artifactId>scf-core</artifactId>
        </dependency>
        <dependency>
            <groupId>org.mydotey.scf</groupId>
            <artifactId>scf-yaml</artifactId>
        </dependency>
    </dependencies>
```

## Usage

```yaml
booleanProperty: yes
intProperty: 1
longProperty: 222222222222222222
stringProperty: s
listProperty: [ v1, v2 ]
mapProperty: { k1: v1, k2: v2 }
objProperty:
    f1: v1
    f2: 10
    f3:
        - 1
        - 2
        - 3
```

```java
@Test
public void testDemo() {
    // create a scf yaml configuration source
    YamlFileConfigurationSourceConfig sourceConfig = new YamlFileConfigurationSourceConfig.Builder()
            .setName("yaml-file").setFileName("test.yaml").build();
    YamlFileConfigurationSource source = new YamlFileConfigurationSource(sourceConfig);

    // create scf manager & properties facade tool
    ConfigurationManagerConfig managerConfig = ConfigurationManagers.newConfigBuilder()
        .setName("my-app").addSource(1, source).build();
    ConfigurationManager manager = ConfigurationManagers.newManager(managerConfig);

    PropertyConfig<String, Boolean> propertyConfig1 =
        ConfigurationProperties.<String, Boolean> newConfigBuilder()
        .setKey("booleanProperty").setValueType(Boolean.class).setDefaultValue(false).build();
    boolean booleanValue = manager.getPropertyValue(propertyConfig1);
    System.out.println(booleanValue);

    PropertyConfig<String, Integer> propertyConfig2 =
        ConfigurationProperties.<String, Integer> newConfigBuilder()
        .setKey("intProperty").setValueType(Integer.class).setDefaultValue(0).build();
    int intValue = manager.getPropertyValue(propertyConfig2);
    System.out.println(intValue);

    PropertyConfig<String, Long> propertyConfig3 =
        ConfigurationProperties.<String, Long> newConfigBuilder()
        .setKey("longProperty").setValueType(Long.class).setDefaultValue(0L).build();
    long longValue = manager.getPropertyValue(propertyConfig3);
    System.out.println(longValue);

    PropertyConfig<String, String> propertyConfig4 =
        ConfigurationProperties.<String, String> newConfigBuilder()
        .setKey("stringProperty").setValueType(String.class).build();
    String stringValue = manager.getPropertyValue(propertyConfig4);
    System.out.println(stringValue);

    @SuppressWarnings({ "rawtypes", "unchecked" })
    PropertyConfig<String, List<String>> propertyConfig5 = ConfigurationProperties
            .<String, List<String>> newConfigBuilder().setKey("listProperty")
            .setValueType((Class<List<String>>) (Class) List.class).build();
    List<String> listValue = manager.getPropertyValue(propertyConfig5);
    System.out.println(listValue);

    @SuppressWarnings({ "rawtypes", "unchecked" })
    PropertyConfig<String, Map<String, String>> propertyConfig6 = ConfigurationProperties
            .<String, Map<String, String>> newConfigBuilder().setKey("mapProperty")
            .setValueType((Class<Map<String, String>>) (Class) Map.class).build();
    Map<String, String> mapValue = manager.getPropertyValue(propertyConfig6);
    System.out.println(mapValue);

    PropertyConfig<String, TestPojo> propertyConfig7 =
        ConfigurationProperties.<String, TestPojo> newConfigBuilder()
        .setKey("objProperty").setValueType(TestPojo.class).build();
    TestPojo objValue = manager.getPropertyValue(propertyConfig7);
    System.out.println(objValue);
}
```
