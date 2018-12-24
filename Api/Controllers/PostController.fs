namespace SimpleCms.Controllers

open Logic
open Microsoft.AspNetCore.Mvc
open Models.Models
open System.Collections.Generic

type IPostController =
    abstract GetAll : unit -> IEnumerable<Post>
    abstract Save : Post -> bool
    abstract Get : string -> Post
    abstract Delete : string -> bool
    abstract Update : string * Post -> bool

[<Route("api/[controller]")>]
type PostController(logic : IPostLogic) =
    inherit Controller()
    member this.logic = logic

    [<Route("")>]
    [<HttpGet>]
    member this.GetAll() = 
        this.logic.GetAll()
    
    [<Route("{id}")>]
    [<HttpGet>]
    member this.Get([<FromRoute>] id) = 
        this.logic.Get(id)
    
    [<Route("")>]
    [<HttpPost>]
    member this.Save([<FromBody>] obj) = 
        this.logic.Save(obj)
    
    [<Route("{id}")>]
    [<HttpDelete>]
    member this.Delete([<FromRoute>] id) = 
        this.logic.Delete(id)
    
    [<Route("{id}")>]
    [<HttpPut>]
    member this.Update([<FromRoute>] id, [<FromBody>] obj) = 
        this.logic.Update(id, obj)
