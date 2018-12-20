namespace SimpleCms.Controllers

open Microsoft.AspNetCore.Mvc

[<Route("")>]
type HomeController() =
    inherit Controller()

    [<Route("")>]
    [<HttpGet>]
    member this.Index() =
        Ok("Hello World!")