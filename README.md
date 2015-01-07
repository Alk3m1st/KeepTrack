KeepTrack
=========

ASP MVC, Azure Table Storage and Angular task tracker.

Ready to deploy straight to an Azure Website. Just needs a SQL DB for User Accounts and an Azure Storage account for the Items themselves. Config can be set via the Azure Portal to keep details out of the source code.

Needs...
* Finish moving from Repo pattern to Command-Query Pattern.
* Lose the horrible default template, use Twitter Bootstrap or similar responsive framework instead.
* Convert main Controller into WebApi Controller and make endpoints RESTful.
* Use Angular filtering on items for quick searching.
* Add Pagination or just limit to a certain length. Maybe even detect scrolling near bottom to load more items.