module Board

open ImmutableArray

let EmptyBoard() = 
   let data = Array2D.init 8 8 (fun _ _ -> None)
   ImmutableArray2D(data)
   