# Reactive Programming: changing the world at Netflix, Microsoft, Slack and beyond!

Matthew Podwysocki at AngularConf

<div class="youtube-video-container"><iframe src="https://www.youtube.com/embed/yEeDbHvg1vQ" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>

## Duality

Erik Meijer's keynote argues that `IObservable<T>` and `IEnumerable<T>` are
mathematical duals — push-based and pull-based collections that share the same
shape with the arrows reversed. That single insight is what lets ReactiveUI use
LINQ operators (`Select`, `Where`, `GroupBy`) over event streams, and is the
reason `WhenAnyValue(...).Select(...)` reads like LINQ-to-Objects.

The full write-up: [Meijer.duality.pdf](https://csl.stanford.edu/~christos/pldi2010.fit/meijer.duality.pdf).

<div class="youtube-video-container"><iframe src="https://www.youtube.com/embed/looJcaeboBY" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe></div>
