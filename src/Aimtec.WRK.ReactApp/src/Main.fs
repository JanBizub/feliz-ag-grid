module Main
open AppRouter
open Elmish
open Elmish.Debug
open Elmish.React
open Elmish.Navigation
open Elmish.UrlParser
open Elmish.HMR // Elmish.HMR must be last open statement in order to HMR works correctly. 

Program.mkProgram AppState.init AppState.update AppView.Render
|> Program.toNavigable (parseHash pageParser) urlUpdate
#if DEBUG
|> Program.withConsoleTrace
|> Program.withDebugger
#endif
|> Program.withReactBatched "fable-app"
|> Program.run
