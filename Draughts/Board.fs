module Board

open System
open Square
open ImmutableArray

type Board(data) = 
   new() =
      let data = Array2D.init 8 8 (fun col row -> 
         let isEven n = n % 2 = 0
         match isEven row, isEven col with
         | false, false -> Unreachable
         | false, true -> Unoccupied 
         | true, false -> Unoccupied
         | true, true -> Unreachable
         )
      new Board(data)
   member this.Width = data |> Array2D.length1
   member this.Height = data |> Array2D.length2
   member this.Item(col, row) = data.[col, row]
   member this.Set(col, row, value) =
      match value, data.[col, row] with
      | Unoccupied, Occupied _ 
      | Occupied _, Unoccupied ->
         let data' = Array2D.copy data
         data'.[col, row] <- value
         Board(data')
      | Occupied _, Occupied _ -> raise (ArgumentException("Square is already occupied"))
      | Unoccupied, Unoccupied -> raise (ArgumentException("Square is already unoccupied"))
      | Unreachable, _
      | _, Unreachable -> raise(ArgumentException("That square is not reachable"))

   // TODO consider copying tests from ImmutableArray