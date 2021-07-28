# Contributing to Mews.Fiscalizations projects

A big welcome and thank you for considering contributing to Mews.Fiscalizations open source projects! It’s people like you that make it a reality for users in our community.

Reading and following these guidelines will help us make the contribution process easy and effective for everyone involved. It also communicates that you agree to respect the time of the developers managing and developing these open source projects. In return, we will reciprocate that respect by addressing your issue, assessing changes, and helping you finalize your pull requests.

## Quicklinks

* [Code of Conduct](#code-of-conduct)
* [Getting Started](#getting-started)
    * [Issues](#issues)
    * [Pull Requests](#pull-requests)
    * [Code Style](#code-style)

## Code of Conduct

We take our open source community seriously and hold ourselves and other contributors to high standards of communication. By participating and contributing to this project, you agree to uphold our [Code of Conduct](https://github.com/MewsSystems/fiscalizations/blob/master/CODE_OF_CONDUCT.md).

## Getting Started

Contributions are made to this repo via Issues and Pull Requests (PRs). A few general guidelines that cover both:

- To report a bug or request a feature, please create an [issue](https://github.com/MewsSystems/fiscalizations/issues/new/choose) and follow the issue template guidelines.
- Search for existing Issues and PRs before creating your own.

### Issues

Issues should be used to report problems with the library, request a new feature, or to discuss potential changes before a PR is created. When you create a new Issue, a template will be loaded that will guide you through collecting and providing the information we need to investigate.

If you find an Issue that addresses the problem you're having, please add your own reproduction information to the existing issue rather than creating a new one. Adding a [reaction](https://github.blog/2016-03-10-add-reactions-to-pull-requests-issues-and-comments/) can also help be indicating to our maintainers that a particular problem is affecting more than just the reporter.

### Pull Requests

PRs to our libraries are always welcome and can be a quick way to get your fix or improvement slated for the next release. In general, PRs should:

- Follow single responsibility principle, i.e. solving one and exactly one thing. If your PR consists of multiple different changes, split it into separate PRs.
- Every PR should have a description, explaining mainly what has changed and why, so that any developer can quickly understand the purpose of PR without necessarily being involved in every preceding discussion, decisions etc. There is a template set up, so you can follow it.
- Add unit tests for fixed or changed functionality (if a test suite already exists).
- Title of the PR should be explanatory. We usually use the first line of the structured commit.
- Update the documentation in the repo if applicable.
- Every PR should be mergeable.
- Every PR should pass all the actions (tests/security scans..etc).

For changes that address core functionality or would require breaking changes (e.g. a major release), it's best to open an Issue to discuss your proposal first. This is not required but can save time creating and reviewing changes.

### Code Style

Most important rule is that code should be:
- Simple = Easy to understand
- Correct
- Fast

Before making a PR, please follow these guidelines to make sure you follow our coding principles:

1. We write clean code, so we don't just review if the code works, we review how it looks and if it follows our code style rules.
2. We don't just look at what the code does, but also what the code could be doing. So it should not be possible to use the code in an intentionally bad way.
3. We use functional programming principles, please consider taking a look at the library we use [FuncSharp](https://github.com/siroky/FuncSharp).
4. We prefer type guarantees over code-checks.
5. We use immutable DTOs.
6. All endpoints should be Asynchronous.
