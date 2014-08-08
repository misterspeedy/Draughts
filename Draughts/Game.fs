module Game

open System
open Player
open Piece
open Square
open Board

open System

type Game(currentPlayer, board) =
   new() =
      let mutable _board = Board()
      do
         // Position red pieces:
         for row in 0..2 do
            for col in 0.._board.Width-1 do
               if _board.[col, row] = Unoccupied then 
                  _board <- _board.Set(col, row, Occupied (Piece Red))
         // Position white pieces:
         for row in _board.Height-3.._board.Height-1 do
            for col in 0.._board.Width-1 do
               if _board.[col, row] = Unoccupied then 
                  _board <- _board.Set(col, row, Occupied (Piece White))
      Game(FirstPlayer, _board)

   member this.Board = board
   member this.Move(player, fromCol, fromRow, toCol, toRow) =
      if player <> currentPlayer then
         raise (ArgumentException("It is not that player's turn"))
      let piece = board.[fromCol, fromRow]
      let board' = 
         board
            .Set(toCol, toRow, piece)
            .Set(fromCol, fromRow, Unoccupied)
      Game(player |> NextPlayer, board')
   member this.CurrentPlayer = currentPlayer