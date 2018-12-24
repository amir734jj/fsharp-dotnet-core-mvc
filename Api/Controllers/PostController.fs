namespace SimpleCms.Controllers

open Logic
open Microsoft.AspNetCore.Mvc
open Models.Models
open System.Collections.Generic

[<AbstractClass>]
type public AbstractCrudController<'T>() =
    inherit Controller()
    abstract member GetAll : unit -> IEnumerable<'T> 
    abstract member Save : 'T -> bool
    abstract member Get : string -> 'T 
    abstract member Delete : string -> bool
    abstract member Update : string * 'T -> bool
 
[<Route("api/[controller]")>]
type public PostController(logic : IPostLogic) =
    inherit AbstractCrudController<Post>()
    member this.logic = logic

    [<Route("")>]
    [<HttpGet>]
    override this.GetAll() = 
        this.logic.GetAll()
    
    [<Route("{id}")>]
    [<HttpGet>]
    override this.Get([<FromRoute>] id) = 
        this.logic.Get(id)
    
    [<Route("")>]
    [<HttpPost>]
    override this.Save([<FromBody>] obj) = 
        this.logic.Save(obj)
    
    [<Route("{id}")>]
    [<HttpDelete>]
    override this.Delete([<FromRoute>] id) = 
        this.logic.Delete(id)
    
    [<Route("{id}")>]
    [<HttpPut>]
    override this.Update([<FromRoute>] id, [<FromBody>] obj) = 
        this.logic.Update(id, obj)

