IsBlog: true
IsPost: true
Title: Moving towards VSTS and continuous deployment
Tags: Announcement
Lead: Geoffrey Huntley
---

Howdy folks,

It took us eight months to ship ReactiveUI 8.0.0 and to be frank that was way too long. It caused an immeasurable amount of stress that I don't want to ever experience again. It also lost us many users and new contributors/maintainers who could have helped out.  We need to make releases an everyday thing with reduced risk and not a once in a blue moon thing with excessive risk. 

Currently, ReactiveUI has two branches - develop and master. We intend to get rid of the develop branch and only have master. This ensures that maintainers are getting timely feedback from the community about quality and it provides assurances to contributors that time invested into contribution will have a fast ROI.

Ultimately our goal as an open-source project is to reduce the burden on our maintainers and ensure that there are no single points of failure in leadership. By investing in the experience of our contributors, we are investing in people who someday may become maintainers of ReactiveUI.

Once [the end-to-end integration tests ship](https://github.com/reactiveui/ReactiveUI/pull/1605) it opens up a world of possibility where the project can increase velocity without sacrificing quality. Done correctly it will ensure contributors receive immediate value for digging in and helping the project. 

If a consumer is ever "blocked" by a fault in the framework, then the maintainers can have an open and honest discussion requesting the consumer to submit a pull-request to master. This pull-request will automatically generate binaries that the maintainers can use to perform validation (in addition to our automated test suite). 

Maintainers operate under the rule that only pull-requests that are ready for shipping to production are merged. If the feature requires coordination with multiple parties and multiple attempts, then maintainers can work in a feature branch that once complete is merged to master as soon as possible. Feature branches will also generate binaries.

If something is merged that is later found to have caused a regression the immediate course of action for maintainers of ReactiveUI will be to revert the commit and ship a new release. The maintainers then can take as much time as needed to revisit why the regression occurred, resolve it and implement controls to ensure it doesn't happen again. In case that wasn't clear, we roll forward. Always.

Consumers will no longer need to configure the MyGet feed because we are now deploying to NuGet on a regular basis. Maintainers and contributors will still need to configure MyGet as that is where all build artifacts for unmerged code will land.

- We intend to achieve this by moving away from AppVeyor as our needs have outgrown what they can currently offer. ReactiveUI needs to implement automated end-to-end tests that run as desktop apps and mobile apps on multiple operating systems. VSTS has powerful build matrixes that make this a walk in the park.
- We intend to reduce the complexity of our build.cake by removing all release functionality and instead embracing the release management features of VSTS.
- We are NOT moving our source control or issue management to VSTS as GitHub is where the community is.
    
The .NET Foundation provides it's member projects with a subsidized VSTS instance and Oren Novotny has volunteered to configure everything for us but if you want to help out I'm sure he would be keen to pair with you. Step up soon however as he _really_ wants to ship these improvements and if you delay you might just miss the boat. 

![release management](https://user-images.githubusercontent.com/127353/39577512-e39557e0-4f24-11e8-9209-0f049d3f86f1.jpg)

I believe these changes will provide the following benefits:

- Reduced cycle time from pull-request raised until release meaning that people receive value sooner.
- A reduction in cognitive overhead and stress for maintainers by automating manual tasks and allow them to implement an aggressive issue ageing policy.
- An increase in first-time contributors.
- An increase in repeat contributors.
- An increase in contributors becoming maintainers.
- An increase in the amount of engaged community members in Slack.
- An increase in the number of engaged community members authoring blog posts.
- An increase in the number of engaged community members delivering talks.
- An increase in the number of questions asked on StackOverflow.
