import React from 'react';
import { SprkTable, SprkPagination } from '@sparkdesignsystem/spark-react';
import { PagedResult } from '../../models/PagedResult';

interface PaginatedTableProps {
    pagedResult: PagedResult;
    onChange: (newPage: number) => void;
}

const PaginatedTable: React.FC<PaginatedTableProps> = (props) => {
    const columns = [{
        name: 'data1',
        header: 'Column Heading'
      },
      {
        name: 'data2',
        header: 'Column Heading'
      },
      {
        name: 'data3',
        header: 'Column Heading'
      }
    ];
    const rows = [{
        data1: "Data 1",
        data2: "Data 2",
        data3: "Data 3"
      },
      {
        data1: "Data 1",
        data2: "Data 2",
        data3: "Data 3"
      },
      {
        data1: "Data 1",
        data2: "Data 2",
        data3: "Data 3"
      },
      {
        data1: "Data 1",
        data2: "Data 2",
        data3: "Data 3"
      },
    ]
    return (
        <>
            <div className="sprk-u-mtl">
            <SprkTable
                additionalTableClasses="sprk-b-Table--spacing-medium"
                idString="table-1"
                columns={columns}
                rows={rows}
            >
            </SprkTable>
            </div>

            <div className="sprk-u-mtl">
            <SprkPagination
                totalItems={props.pagedResult.totalItems}
                itemsPerPage={props.pagedResult.itemsPerPage}
                currentPage={props.pagedResult.currentPage}
                onChangeCallback={(args: any) => props.onChange(args.newPage)}
            />
            </div>
        </>
    );
}

export default PaginatedTable;
