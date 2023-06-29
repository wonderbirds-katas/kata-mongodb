# Learn MongoDB

[![Gitpod ready-to-code](https://img.shields.io/badge/Gitpod-ready--to--code-blue?logo=gitpod)](https://gitpod.io/#https://github.com/wonderbirds-katas/kata-mongodb)
[![Build Status Badge](https://github.com/wonderbirds-katas/kata-mongodb/workflows/.NET/badge.svg)](https://github.com/wonderbirds-katas/kata-mongodb/actions?query=workflow%3A%22.NET%22)

This kata shows some basics to get started with MongoDB.

## Thanks

Many thanks to [JetBrains](https://www.jetbrains.com/?from=kata-mongodb) who provide
an [Open Source License](https://www.jetbrains.com/community/opensource/) for this project ❤️.

## Development

### What is inside?

- Kata.Logic - is a DLL project for business logic
- Kata.Logic.Tests - is the corresponding xUnit test project, configured with static code rules allowing
  underscores in test names (GlobalSuppressions.cs)
- .github\dependabot.yml - configuration for the GitHub Dependabot.
- .github\workflows\dotnet.yml - automatic builds using GitHub actions.

### Quick-Start

Click the 
[![Gitpod ready-to-code](https://img.shields.io/badge/Gitpod-ready--to--code-blue?logo=gitpod)](https://gitpod.io/#https://github.com/wonderbirds-katas/kata-mongodb)
badge (also above) to launch a web IDE.

If that does not work for you or if you'd like to have the project on your local machine, then continue reading.

### Prerequisites

To compile, test and run this project the [.NET 7.0.x SDK](https://dotnet.microsoft.com/download) is required on
your machine. For calculating code metrics I recommend [metrix++](https://github.com/metrixplusplus/metrixplusplus).
This requires python.

If you are interested in test coverage, then you'll need the following tools installed:

```shell
dotnet tool install --global coverlet.console --configfile NuGet-OfficialOnly.config
dotnet tool install --global dotnet-reportgenerator-globaltool --configfile NuGet-OfficialOnly.config
```

## Build, Test, Run

Run the following commands from the folder containing the `.sln` file in order to build and test.

### Build the Solution and Run the Tests

```sh
dotnet build
dotnet test
```

```shell
# If you like continuous testing then use the dotnet file watcher to trigger your tests
dotnet watch -p ./Kata.Logic.Tests test
```

```shell
# As an alternative, run the tests with coverage and produce a coverage report
rm -r Kata.Logic.Tests/TestResults && \
  dotnet test --no-restore --verbosity normal /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput='./TestResults/coverage.cobertura.xml' && \
  reportgenerator "-reports:Kata.Logic.Tests/TestResults/*.xml" "-targetdir:report" "-reporttypes:Html;lcov" "-title:KataMongoDb"
open report/index.html
```

### Before Creating a Pull Request ...

#### Apply code formatting rules

```shell
# Install https://csharpier.io globally, once
dotnet tool install -g csharpier

# Format code
dotnet csharpier .
```

#### Check Code Metrics

... check code metrics using [metrix++](https://github.com/metrixplusplus/metrixplusplus)

- Configure the location of the cloned metrix++ scripts
  ```shell
  export METRIXPP=/path/to/metrixplusplus
  ```

- Collect metrics
  ```shell
  python "$METRIXPP/metrix++.py" collect --std.code.complexity.cyclomatic --std.code.lines.code --std.code.todo.comments --std.code.maintindex.simple -- .
  ```

- Get an overview
  ```shell
  python "$METRIXPP/metrix++.py" view --db-file=./metrixpp.db
  ```

- Apply thresholds
  ```shell
  python "$METRIXPP/metrix++.py" limit --db-file=./metrixpp.db --max-limit=std.code.complexity:cyclomatic:5 --max-limit=std.code.lines:code:25:function --max-limit=std.code.todo:comments:0 --max-limit=std.code.mi:simple:1
  ```

At the time of writing, I want to stay below the following thresholds:

```text
--max-limit=std.code.complexity:cyclomatic:5
--max-limit=std.code.lines:code:25:function
--max-limit=std.code.todo:comments:0
--max-limit=std.code.mi:simple:1
```

Finally, remove all code duplication. The next section describes how to detect code duplication.

### Remove Code Duplication Where Appropriate

To detect duplicates I use the [CPD Copy Paste Detector](https://pmd.github.io/latest/pmd_userdocs_cpd.html)
tool from the [PMD Source Code Analyzer Project](https://pmd.github.io/latest/index.html).

If you have installed PMD by download & unzip, replace `pmd` by `./run.sh`.
The [homebrew pmd formula](https://formulae.brew.sh/formula/pmd) makes the `pmd` command globally available.

```sh
pmd cpd --minimum-tokens 50 --language cs --files .
```

## References

- GitHub: [wonderbird / dotnet-starter](https://github.com/wonderbird/dotnet-starter) - My dotnet-starter template for new C# katas