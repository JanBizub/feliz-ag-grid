[<RequireQualifiedAccess>]
module CellRendererView
open System
open AppTypes
open Feliz
open Feliz.AgGrid

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
        ]
      ]
    ]
  ]