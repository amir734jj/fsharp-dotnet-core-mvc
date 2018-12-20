namespace Logic

open System.Collections.Generic
open System.Linq
open Models.Models

type IPostLogic =
    abstract member GetAll : unit -> IEnumerable<Post>
    abstract member Save : Post -> bool
    abstract member Get : string -> Post
    abstract member Delete : string -> bool
    abstract member Update : string * Post -> bool

module PostLogicModule =
    open LiteDB
    open Dal

    type PostLogic(dal: IPostDal) =
        member this.dal = dal

        interface IPostLogic with
            override this.GetAll(): IEnumerable<Post> = 
                this.dal.GetAll()
                
            override this.Get(id: string) = 
                this.dal.Get(id)
                
            override this.Save(obj) =
                this.dal.Save(obj)
                
            override this.Delete(id) = 
                this.dal.Delete(id)
                
            override this.Update(id, obj) =
                this.dal.Update(id, obj)
                