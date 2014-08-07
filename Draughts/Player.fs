module Player

type Player = Red | White

let FirstPlayer = Red

let NextPlayer player =
   match player with
   | Red -> White
   | White -> Red