module AppRouter
open Elmish
open Elmish.UrlParser
open AppTypes

let pageParser: Parser<Route -> Route, Route> =
  oneOf [
    map Route.Home           top
    map Route.Simple         (s "Simple")
    map Route.RangeSelection (s "RangeSelection")
    map Route.CellRenderer   (s "CellRenderer")
  ]

let urlUpdate (route: Option<Route>) (state: AppState) =
  match route with
  | None -> 
    { state with CurrentRoute = Route.Invalid }, Cmd.none

  | Some route ->
    match route with
    | Route.Home -> 
      { state with CurrentRoute = route }, Cmd.none
    | Route.Simple -> 
      { state with CurrentRoute = route }, Cmd.none
    | Route.RangeSelection -> 
      { state with CurrentRoute = route }, Cmd.none
    | Route.CellRenderer -> 
      { state with CurrentRoute = route }, Cmd.none
    | Route.Invalid -> 
      { state with CurrentRoute = route }, Cmd.none
