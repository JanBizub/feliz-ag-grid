[<RequireQualifiedAccess>]
module CellRendererView
open System
open Feliz
open Feliz.AgGrid
open AppTypes

let console = Browser.Dom.console

[<ReactComponent>]
let RenderAgGrid (state: AppState) dispatch =
  Html.div [
    prop.classes [ ThemeClass.Alpine; "ag-grid-container" ]
    prop.children [
      AgGrid.grid [
        AgGrid.frameworkComponents   {| btnCellRenderer = BtnCellRenderer.Render |}
        AgGrid.reactUi               true
        AgGrid.immutableData         true
        AgGrid.modules               [| clientSideRowModelModule |]
        AgGrid.rowData               state.GridData

        AgGrid.columnDefs [
          ColumnDef.create<Guid> [
            ColumnDef.headerName  "ID"
            ColumnDef.valueGetter (fun d -> d.Id)
          ]

          ColumnDef.create<string> [
            ColumnDef.headerName  "Name"
            ColumnDef.editable    (fun _ -> true)
            ColumnDef.valueGetter (fun d -> d.Name)
          ]

          ColumnDef.create<int> [
            ColumnDef.headerName  "Ammount"
            ColumnDef.valueGetter (fun d -> d.Ammount)
          ]
  
          ColumnDef.create<int option> [
            ColumnDef.headerName  "AmmoInMagazine"
            ColumnDef.valueGetter (fun d -> d.AmmoInMagazine)
          ]

          ColumnDef.create<unit> [
           ColumnDef.headerName         "DeleteGridLine"
           ColumnDef.headerTooltip      "Delete"
           ColumnDef.width              125
           // Cell renderer
           ColumnDef.cellRenderer       "btnCellRenderer"
           ColumnDef.cellRendererParams {| Text    = "Delete";
                                           Classes = [|"btn"; "btn-primary"|];
                                           OnClick = (fun _ -> Browser.Dom.console.log "Button clicked") |}
          ]
        ]
      ]
    ]
  ]