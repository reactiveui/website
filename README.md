# ReactiveUI Website

![Build website](https://github.com/reactiveui/website/workflows/Build%20website/badge.svg)

This is the ReactiveUI website. It's a static site generated by [Statiq](https://www.statiq.dev/docs).

## Contributing

**Steps**
1. Fork the current project
2. Create a new branch, if needed
3. Clone the project
4. In order to build and host the docs do the following:
- **Windows** Open command prompt at the repository root folder and run `dotnet run -- preview`
- **Linux** Open your favorite shell at the repository root folder and run `dotnet run -- preview`
5. Wait several minutes while it installs dependencies and initializes (approx 3-5 mins).  It is ready when you see `Hit Ctrl-C to exit`
6. Browse the website on `localhost:5080`

## Adding a new blog post

1. Create a new file in the `input/posts` folder.  The file name should be in the format `yyyy-MM-dd-Title.md`.

2. Add the following metadata to the top of the file:

```
NoTitle: true
IsBlog: true
Title: Title of the blog post
Published: yyyy-MM-dd
Author: Your name
Tags: Release Notes, Announcement, Article
---
```

3. Write your blog post using markdown.
4. Submit a pull request.

## Adding a new page

1. Create a new file in the `input/#RELEVANT SECTION#` folder.  The file name should be in the format `Title.md`.
2. Add the following metadata to the top of the file:

```
NoTitle: true
Title: Title of the page
---
```

3. Write your page using markdown.
4. Submit a pull request.

## Adding a new section

1. Create a new folder in the `input` folder.  The folder name should be in the format `#RELEVANT SECTION#`.
2. Create a new file in the `input/#RELEVANT SECTION#` folder.  The file name should be in the format `Title.md`.
3. Add the following metadata to the top of the file:

```
NoTitle: true
Title: Title of the page
---
```

4. Write your page using markdown.
5. Submit a pull request.

## Configuring the website

The website is configured using the `settings.yml` file.  The following settings are available:

```yml
SiteTitle: ReactiveUI - The Name of the site
Logo: /assets/img/logo.png - The logo to use in the header
Host: reactiveui.net - The host name of the site
LinksUseHttps: true - Whether to use https for links
NetlifyRedirects: true - Whether to generate netlify redirects
EditRoot: https://github.com/reactiveui/website/edit/main/input - The root url for editing pages
FontLink: "https://fonts.googleapis.com/css2?family=Dosis:wght@700&family=Source+Sans+Pro:wght@300;400&display=swap" - The font to use
sass-blue: "#48bdfe" - The blue color to use
sass-indigo: "#bf4bff" - The indigo color to use
sass-purple: "#bf4bff" - The purple color to use
sass-pink: "#ff5885" - The pink color to use
sass-red: "#ff5885" - The red color to use
sass-orange: "#e98f00" - The orange color to use
sass-yellow: "#e98f00" - The yellow color to use
sass-green: "#06c0af" - The green color to use
sass-teal: "#06c0af" - The teal color to use
sass-cyan: "#06c0af" - The cyan color to use
sass-primary: "#319af3" - The primary color to use
sass-secondary: "#319af3" - The secondary color to use
sass-success: "#5cb85c" - The success color to use
sass-info: "#319af3" - The info color to use
sass-warning: "#f0ad4e" - The warning color to use
sass-danger: "#d9534f" - The danger color to use
sass-font-family-sans-serif: "\"Source Sans Pro\", Helvetica, Arial, sans-serif;" - The font family to use for sans-serif
sass-font-family-serif: "\"Source Sans Pro\", Helvetica, Arial, sans-serif;" - The font family to use for serif
sass-font-family-monospace: "\"Source Sans Pro\", Helvetica, Arial, sans-serif;" - The font family to use for monospace
sass-font-family-base: "\"Source Sans Pro\", Helvetica, Arial, sans-serif;" - The font family to use for base
PostTitle: Blog - The title to use for blog posts
PostOrder: 1 - The Navigation menu order to use for the blog / posts section
ApiLayout: Api/_ApiLayout - The layout to use for api pages
ApiOrder: 1 - The Navigation menu order to use for the API section
PostSources: posts/**/* - The sources to use for blog posts
PageSources: pages/**/* - The sources to use for pages
```

