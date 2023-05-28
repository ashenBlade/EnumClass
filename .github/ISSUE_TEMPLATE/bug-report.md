---
name: Bug Report
about: Create a bug report to help us improve
title: ''
labels: bug
assignees: ''
---

### Describe the bug
A clear and concise description of what the bug is

e.g. Assigned null instead of string to `EnumClass`'s `Namespace` property and it caused exception on compilation

### Steps to Reproduce
Provide simple steps to reproduce the behavior

e.g.:
1. Declare enum
```csharp
using EnumClass.Attributes;

namespace Test;

[EnumClass(ClassName = null)]
public enum SampleEnum
{
    First
}
```
2. Build project
3. Call `xxx` method
```csharp
SampleEnum.GetAllMembers();
```


### Expected behavior
A clear and concise description of what you expected to happen.

e.g. `NullReferenceExcpetion` was thrown during build by generator


### Actual behaviour
What you expect to be done

e.g. `null` value was ommited or diagnostic reported 

### Environment

- .NET version (6.0.103, 7.0.15)
- OS (Windows, Linux)
- IDE (Visual Studio, Rider)

### Context

Provide additional items, such as logs, that would help to solve this issue
