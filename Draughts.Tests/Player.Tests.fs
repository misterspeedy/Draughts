[<NUnit.Framework.TestFixture>]
module PlayerTests

open System
open NUnit.Framework
open Player

[<Test>]
let ``Play starts with red``() =
   let expected = Red
   let actual = FirstPlayer
   Assert.AreEqual(expected, actual)

[<Test>]
let ``White plays after red``() =
   let expected = White
   let current = Red
   let actual = current |> NextPlayer 
   Assert.AreEqual(expected, actual)
  
[<Test>]
let ``Red plays after white``() =
   let expected = Red
   let current = White
   let actual = current |> NextPlayer 
   Assert.AreEqual(expected, actual)