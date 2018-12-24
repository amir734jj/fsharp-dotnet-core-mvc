namespace SimpleCms.Controllers

open Microsoft.AspNetCore.Mvc

[<ApiExplorerSettings(IgnoreApi = true)>]
[<Route("")>]
type HomeController() =
    inherit Controller()

    [<Route("")>]
    [<HttpGet>]
    member this.Index() =
        "Hello World!"