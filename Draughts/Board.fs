module Board

open System
open Square
open ImmutableArray

type Board(data) = 
   new() =
      let data = Array2D.init 8 8 (fun col row -> 
         let isOdd c r = 
            let index = r * 8 + c
            index % 2 = 1
         if isOdd col row then
            Square.Unoccupied
         else
            Square.Unreachable)
      new Board(data)
   member this.Width = data |> Array2D.length1
   member this.Height = data |> Array2D.length2
   member this.Item(col, row) = data.[col, row]
   member this.Set(col, row, value) =
      match data.[col, row] with
      | Unoccupied -> 
         let data' = Array2D.copy data
         data'.[col, row] <- value
         Board(data')
      | Occupied _ -> raise (ArgumentException("Square is already occupied"))
      | _ -> raise(ArgumentException("Case not yet supported"))

   // TODO consider copying tests from ImmutableArray