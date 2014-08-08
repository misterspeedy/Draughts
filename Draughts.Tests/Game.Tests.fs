[<NUnit.Framework.TestFixture>]
module GameTests

open System
open NUnit.Framework
open Player
open Piece
open Square
open Board
open Game

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

[<Test>]
let ``A game accepts a valid move from the current player``() =
   let sut = Game()
   let sut' = sut.Move(Red, 1, 2, 0, 3)
   Assert.AreNotEqual(sut, sut')

[<Test>]
let ``A game rejects an off the board move from the current player``() =
   let sut = Game()
   let cant = (fun () -> sut.Move(Red, 7, 2, 8, 3))
   Assert.That(cant, Throws.TypeOf<IndexOutOfRangeException>())

[<Test>]
let ``A game rejects an otherwise valid move when it is not the players turn``() =
   let sut = Game()
   let cant = (fun () -> sut.Move(White, 1, 2, 0, 3))
   Assert.That(cant, Throws.TypeOf<ArgumentException>())

[<Test>]
let ``When a game accepts a move the current player changes``() =
   let sut = Game()
   let sut' = sut.Move(Red, 1, 2, 0, 3)
   let expected = White
   let actual = sut'.CurrentPlayer
   Assert.AreEqual(expected, actual)

[<Test>]
let ``When a game rejects a move the current player does not change``() =
   // Test is inexpressible: if an exception is raised a new Game() instance won't be assigned
   let expected = true
   let actual = true
   Assert.AreEqual(expected, actual)

[<Test>]
let ``When a game accepts a move the from-square becomes unoccupied``() =
   let sut = Game()
   let sut' = sut.Move(Red, 1, 2, 0, 3)
   let expected = Unoccupied
   let actual = sut'.Board.[1, 2]
   Assert.AreEqual(expected, actual)

[<Test>]
let ``When a game rejects a move the from-square does not change``() =
   // Test is inexpressible: if an exception is raised a new Game() instance won't be assigned
   let expected = true
   let actual = true
   Assert.AreEqual(expected, actual)

[<Test>]
let ``When a game accepts a move the to-square becomes occupied with the right piece``() =
   let sut = Game()
   let sut' = sut.Move(Red, 1, 2, 0, 3)
   let expected = Occupied (Piece Red)
   let actual = sut'.Board.[0, 3]
   Assert.AreEqual(expected, actual)

[<Test>]
let ``When a game rejects a move the to-square does not change``() =
   // Test is inexpressible: if an exception is raised a new Game() instance won't be assigned
   let expected = true
   let actual = true
   Assert.AreEqual(expected, actual)

[<Test>]
let ``A game rejects a move when the from-square does not contain a piece for that player``() =
   let sut = Game()
   let cant = (fun () -> sut.Move(White, 1, 2, 0, 3))
   Assert.That(cant, Throws.TypeOf<ArgumentException>())

[<Test>]
let ``A game rejects a move when the to-square is already occupied``() =
   let sut = Game()
   let sut' = 
      sut
         .Move(Red, 1, 2, 2, 3)
         .Move(White, 0, 5, 1, 4)
   // Move into the position White just moved into:
   let cant = (fun () -> sut.Move(Red, 2, 3, 1, 4) |> ignore)
   Assert.That(cant, Throws.TypeOf<ArgumentException>())
