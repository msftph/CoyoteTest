# Coyote Test Example

provies an example of using coyote in msbuild

# Getting Started

run the dotnet tool restore command to install the coyote cli

```
dotnet tool restore
```

# Rewrite - How does it work?

## msbuild properties

We set the coyote-rewrite.json file to "Copy Always" in the ms build properties

```xml
<ItemGroup>
  <None Update="coyote-rewrite.json">
    <CopyToOutputDirectory>Always</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

We set the PostBuild target to run the `dotnet tool coyote rewrite` command after the test assembly is built

```xml
<Target Name="CoyoteRewrite" AfterTargets="AfterBuild">
    <Exec Command="dotnet tool run coyote rewrite $(OutputPath)\coyote-rewrite.json" />
</Target>
```

## coyote-rewrite.json

The coyote rewrite file points to the assemblies we want to rewrite [coyote-rewrite.json](coyote-rewrite.json)

```json
{
  "AssembliesPath": ".",
  "OutputPath": ".",
  "Assemblies": [
    "CoyoteTest.LibraryExample.dll",
    "CoyoteTest.LibraryExample.Tests.dll"
  ]
}
```

# instrument test

Use the TestEngine example [here](https://microsoft.github.io/coyote/#how-to/unit-testing/#integrate-coyote-with-a-unit-testing-framework) to create tests