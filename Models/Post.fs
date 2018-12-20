namespace Models

module Models =
    open System
    open System.ComponentModel.DataAnnotations
    
    type Post() =
        [<Key>]
        member val Id: string = null with get, set
        member val Subject: string = null with get, set
        member val Date: DateTime = DateTime.Now with get, set
        member val Text: string = null with get, set
