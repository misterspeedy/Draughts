[<NUnit.Framework.TestFixture>]
module GameTests

open System
open NUnit.Framework
open Piece
open Game
open Board
open Square

[<Test>]
let ``A game has a board``() =
   let sut = Game()
   let board = sut.Board
   Assert.IsNotNull(board)

[<Test>]
let ``A new game sets up the board with three rows of red pieces at the top``() =
   let sut = Game()
   let expected = Piece Red
   for row in 0..2 do
      for col in 0..sut.Board.Width-1 do
         match sut.Board.[col, row] with
         | Unreachable -> ()
         | Occupied actual -> Assert.AreEqual(expected, actual)
         | Unoccupied -> raise (Exception("No square in first three rows should be unoccupied"))

[<Test>]
let ``A new game sets up the board with three rows of white pieces at the bottom``() =
   let sut = Game()
   let expected = Piece White
   for row in sut.Board.Height-3..sut.Board.Height-1 do
      for col in 0..sut.Board.Width-1 do
         match sut.Board.[col, row] with
         | Unreachable -> ()
         | Occupied actual -> Assert.AreEqual(expected, actual)
         | Unoccupied -> raise (Exception("No square in first three rows should be unoccupied"))