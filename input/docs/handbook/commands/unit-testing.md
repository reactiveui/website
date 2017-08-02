# Unit Testing

Read http://kent-boogaart.com/blog/using-the-visual-studio-test-runner-for-mobile-development

Don't mock ReactiveCommands.

ReactiveCommand itself is already designed around testability. Also, the likelihood that you will correctly mock ReactiveCommand semantics via Moq is pretty low, it's a pretty complicated class (and if you did, you would end up doing a ton of unnecessary work).
