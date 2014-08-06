module Board

open ImmutableArray

let EmptyBoard() = 
   let data = Array2D.init 8 8 (fun c r -> 
      let isOdd = 
         let index = r * 8 + c
         index % 2 = 0   
      if isOdd then
         Square.Unreachable
      else
         Square.Unoccupied)
   ImmutableArray2D(data)
   