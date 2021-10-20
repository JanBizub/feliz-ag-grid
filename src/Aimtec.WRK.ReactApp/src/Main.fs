module Main
open Elmish
open Elmish.Debug
open Elmish.React
open Elmish.Navigation
open Elmish.HMR // Elmish.HMR must be last open statement in order to HMR works correctly. 

Program.mkProgram AppState.init AppState.update AppView.Render
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "fable-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
