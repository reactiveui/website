Title: Deleting Merged Braches
----

1. To protect against mistakes (so you can push if you delete something you shouldn't) make sure your local repo is up to date

```shell
git checkout develop
git pull origin develop
for remote in `git branch -r `; do git branch --track $remote; done
```

2. From linux/macOS do a dry run of `script/clean-merged-branches`

```shell
script$ ./clean-merged-branches
warning: refname 'origin/main' is ambiguous.
These branches will be deleted:
  activation
  add_ireactivecommand
  add_reactive_page_view_controller
  add_rx_collection_view
  add_uitextview_binding
  android-reactive-pageradapter
  common-version-location
  control-fetcher-fix
  issues/513/BindWithFuncConverters
  listadapter-observer
  make_derived_collection_schedulable
  no-more-scheduling
  recursive-activation
  rxcoll-must-be-scheduled
  rxui5-master
  rxui6-master
  support_string_headers
  xamarin-forms-example
  xaml-memleaks
Run `./clean-merged-branches -f' if you're sure.
```

3. Now do it for real

```shell
script$ ./clean-merged-branches -f
warning: refname 'origin/main' is ambiguous.
To git@github.com:reactiveui/ReactiveUI.git
 - [deleted]         activation
 - [deleted]         add_ireactivecommand
 - [deleted]         add_reactive_page_view_controller
 - [deleted]         add_rx_collection_view
 - [deleted]         add_uitextview_binding
 - [deleted]         android-reactive-pageradapter
 - [deleted]         common-version-location
 - [deleted]         control-fetcher-fix
 - [deleted]         issues/513/BindWithFuncConverters
 - [deleted]         listadapter-observer
 - [deleted]         make_derived_collection_schedulable
 - [deleted]         no-more-scheduling
 - [deleted]         recursive-activation
 - [deleted]         rxcoll-must-be-scheduled
 - [deleted]         rxui5-master
 - [deleted]         rxui6-master
 - [deleted]         support_string_headers
 - [deleted]         xamarin-forms-example
 - [deleted]         xaml-memleaks
```
