---
NoTitle: true
IsBlog: true
Title: Automatic closure of stale GitHub issues
Tags: Announcement
Author: Geoffrey Huntley
Published: 2017-08-25
---

One of the perks of being a maintainer of an open-source project is you get to be part of the fantastic initiative by GitHub - [maintainers.github.com](https://maintainers.github.com) where some of the brightest minds in open-source leadership from across disciplines/ecosystems share their insights on how to run projects, productively at scale towards successful outcomes. 

One of the lessons we have learned from that community is that having stale issues (or pull-requests) is a source of contention for both maintainers and consumers of software. 

Moments ago we enabled `probot/stale` on our GitHub repository. What this means is a friendly robot will automatically close any open GitHub issue (including pull-request) with more than sixty-seven days of inactivity.  Eighty-five issues were visited by the robot and it asked the initiators what the status is - automatically. As a maintainer, it always feels awkward closing old issues or half-completed pull requests. We haven't always been the best with triaging and this is the first step of many to improve in this area. 

I've gone through by hand and where appropriate marked some of them to never be automatically closed (legitimate bugs or outstanding pull-requests), but if I've missed one, please accept our apologies if your contribution has not been attended to properly and perform some form of activity (comment, label, assign to milestone) to reset the clock.

As is always with open-source, if you want an issue progressed faster please start talks with a maintainer with how you can help out. There are so many ways to contribute to open-source and the most valued are often not code related. We are always looking for more help and look after those who stick around. Say howdy to one of the maintainers or browse [reactiveui.net/contribute](contribute/index.md) for ideas on where to start your journey.

Thank-you
