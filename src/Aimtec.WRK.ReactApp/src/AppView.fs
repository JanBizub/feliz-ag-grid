[<RequireQualifiedAccess>]
module AppView
open System
open Feliz
open AppTypes
open Feliz.AgGrid
open Fable.Core.JsInterop
open Elmish.Navigation

importAll "./../node_modules/bootstrap/dist/css/bootstrap.min.css"

let console = Browser.Dom.console

[<ReactComponent>]
let BtnCellRenderer props =
  Html.button [
    prop.classes [| "btn"; "btn-primary"|]
    prop.text "button text"
  ]

[<ReactComponent>]
let RenderAgGrid (state: AppState) dispatch =
  Html.div [
    prop.classes [ ThemeClass.Balham; "ag-grid-container" ]
    prop.children [
      AgGrid.grid [
        AgGrid.onGridReady (fun _ -> 
          Browser.Dom.console.log ("Grid Ready")
        )

        AgGrid.reactUi               true
        AgGrid.immutableData         true
        AgGrid.modules               [| clientSideRowModelModule |]
        AgGrid.rowData               state.GridData
        AgGrid.components            {| btnCellRenderer = BtnCellRenderer |}

        // https://www.ag-grid.com/react-grid/range-selection-fill-handle/
        AgGrid.rowSelection          Multiple
        AgGrid.enableRangeSelection  true // todo: not working!
        AgGrid.enableFillHandle      true
        AgGrid.enableRangeHandle     true

        AgGrid.columnDefs [
          ColumnDef.create<Guid> [
            ColumnDef.headerName "ID"
            ColumnDef.valueGetter (fun d -> d.Id)
          ]

          ColumnDef.create<string> [
            ColumnDef.headerName "Name"
            ColumnDef.editable (fun _ -> true)
            ColumnDef.valueGetter (fun d -> d.Name)
          ]

          ColumnDef.create<int> [
            ColumnDef.headerName "Ammount"
            ColumnDef.valueGetter (fun d -> d.Ammount)
          ]
  
          ColumnDef.create<int option> [
            ColumnDef.headerName "AmmoInMagazine"
            ColumnDef.valueGetter (fun d -> d.AmmoInMagazine)
          ]

          ColumnDef.create<unit> [
           ColumnDef.headerName         "DeleteGridLine"
           ColumnDef.headerTooltip      "Delete Grid Line"
           ColumnDef.width              55
           //ColumnDef.cellRenderer       "btnCellRenderer"
           //ColumnDef.cellRenderer       (fun value row  -> BtnCellRenderer value )
           //ColumnDef.cellRendererParams {| onClick = (fun _ -> console.log "button clicked"); text = "Button Text" |}
          ]

          //ColumnDef.create<decimal> [
          //  ColumnDef.headerName "Caliber"
          //  ColumnDef.valueGetter (fun d -> d.Caliber)
          //]

          //ColumnDef.create<DateTime> [
          //  ColumnDef.headerName "DateServiced"
          //  ColumnDef.valueGetter (fun d -> d.DateServiced)
          //]
        ]
      ]
    ]
  ]

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
  let header = RenderHeader dispatch

  match state.CurrentRoute with
  | Route.Home -> React.fragment [
      header
      Html.h1 "Home"
    ]

  | Route.Simple -> React.fragment [
      header
      Html.h1 "Simple"
    ]

  | Route.RangeSelection -> React.fragment [
      header
      Html.h1 "RangeSelection"
    ]

  | Route.CellRenderer -> React.fragment [
      header
      Html.h1 "CellRenderer"
    ]

  | Route.Invalid -> React.fragment [
      header
      Html.h1 "Invalid"
    ]