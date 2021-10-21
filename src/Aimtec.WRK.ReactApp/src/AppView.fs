[<RequireQualifiedAccess>]
module AppView
open Fable.Core.JsInterop
open Feliz
open AppTypes

importAll "./../node_modules/bootstrap/dist/css/bootstrap.min.css"

let console = Browser.Dom.console

[<ReactComponent>]
let RenderHeader dispatch = Html.div [
  prop.classes ["btn-group"]
  prop.children [
    Html.button [
      prop.classes [ "btn"; "btn-primary" ]
      prop.onClick (fun _ -> NavigateHome |> dispatch)
      prop.text   "Home"
    ]
    Html.button [
      prop.classes [ "btn"; "btn-primary" ]
      prop.onClick (fun _ -> NavigateSimple |> dispatch)
      prop.text   "Simple"
    ]
    Html.button [
      prop.classes [ "btn"; "btn-primary" ]
      prop.onClick (fun _ -> NavigateRangeSelection |> dispatch)
      prop.text   "RangeSelection"
    ]
    Html.button [
      prop.classes [ "btn"; "btn-primary" ]
      prop.onClick (fun _ -> NavigateCellRenderer |> dispatch)
      prop.text   "CellRenderer"
    ]
  ]
]

[<ReactComponent>]
let Render (state: AppState) dispatch =
  let header          = RenderHeader dispatch
  let homePageV       = (state, dispatch) ||> HomePageView.RenderAgGrid
  let simpleV         = (state, dispatch) ||> SimpleGridView.RenderAgGrid
  let rangeSelectionV = (state, dispatch) ||> RangeSelectionView.RenderAgGrid
  let cellRendererV   = (state, dispatch) ||> HomePageView.RenderAgGrid

  match state.CurrentRoute with
  | Route.Home -> React.fragment [
      header
      homePageV
    ]

  | Route.Simple -> React.fragment [
      header
      simpleV
    ]

  | Route.RangeSelection -> React.fragment [
      header
      rangeSelectionV
    ]

  | Route.CellRenderer -> React.fragment [
      header
      cellRendererV
    ]

  | Route.Invalid -> React.fragment [
      header
      Html.h1 "Invalid"
    ]