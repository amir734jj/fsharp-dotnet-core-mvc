namespace Dal

open System.Collections.Generic
open System.Linq
open Models.Models

type IPostDal =
    abstract member GetAll : unit -> IEnumerable<Post>
    abstract member Save : Post -> bool
    abstract member Get : string -> Post
    abstract member Delete : string -> bool
    abstract member Update : string * Post -> bool


module PostDalModule =
    open LiteDB
    open System;
    open System.Collections.Generic;
    open System.Linq;

    type PostDal(database: LiteDatabase) =
        member this.database = database
        member this.collection = this.database.GetCollection<Post>("Post")
        
        interface IPostDal with
            override this.GetAll(): IEnumerable<Post> = 
                this.collection.FindAll()
                
            override this.Get(id: string) = 
                this.collection.Find(fun x -> x.Id = id).FirstOrDefault()
                
            override this.Save(obj) =
                this.collection.Insert(obj) |> ignore
                true
                
            override this.Delete(id) = 
                this.collection.Delete(fun x -> x.Id = id) |> ignore
                true
                
            override this.Update(id, obj) =
                this.collection.Update(null, obj) |> ignore
                true