# Concurrency Testing using Coyote Framework

This repository contains a set of Coyote program benchmarks together with Microsoft's Coyote project as a submodule.

## Contents of the repository:

- `Coyote` - keeps a copy of [Microsoft's Coyote framework] (https://github.com/microsoft/coyote) (commit b6d3a83). 
	- 	This repository will extend Coyote with a new concurrency testing algorithm.
	-  The folder will be replaced by a submodule for Coyote when we make the scheduler's source code public

- `Benchmarks` - keeps Coyote benchmark applications


## Concurrency testing of a benchmark application:

- Build the `Coyote` project. This will create the build files in `Coyote/bin`.
- Remove `Benchmarks/bin` if nonempty, and build the benchmark project
- Run `Benchmarks/TestDriver` project to test the benchmark application

