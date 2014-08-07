module Game

open System
open Player
open Piece
open Square
open Board

open System

type Game() =
   let mutable currentPlayer = FirstPlayer
   let mutable _board = Board()
   do
      // TODO eliminate repetition
      for row in 0..2 do
         for col in 0.._board.Width-1 do
            match _board.[col, row] with
            | Unreachable -> ()
            | Occupied _ -> raise (Exception("No square in first three rows should be occupied"))
            | Unoccupied -> _board <- _board.Set(col, row, Occupied (Piece Red))
      for row in _board.Height-3.._board.Height-1 do
         for col in 0.._board.Width-1 do
            match _board.[col, row] with
            | Unreachable -> ()
            | Occupied _ -> raise (Exception("No square in first three rows should be occupied"))
            | Unoccupied -> _board <- _board.Set(col, row, Occupied (Piece White))

   member this.Board = _board
   member this.Move(player, fromCol, fromRow, toCol, toRow) =
      if player <> currentPlayer then
         raise (ArgumentException("It is not that player's turn"))
      let piece = _board.[fromCol, fromRow]
      _board <- _board.Set(toCol, toRow, piece)