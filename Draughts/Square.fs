module Square

open Piece

type Square = 
   | Unreachable
   | Unoccupied
   | Occupied of Piece