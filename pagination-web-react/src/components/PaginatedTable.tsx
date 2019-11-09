import React from 'react';
import { SprkTable, SprkPagination } from '@sparkdesignsystem/spark-react';
import { PagedResult } from '../models/PagedResult';

interface PaginatedTableProps {
    pagedResult: PagedResult | undefined;
    onChange: (newPage: number) => void;
}

const PaginatedTable: React.FC<PaginatedTableProps> = (props) => {
    return (
        <>
          {
            props.pagedResult !== undefined &&
            <>
              <div className="sprk-u-mtl">
                <SprkTable
                    additionalTableClasses="sprk-b-Table--spacing-medium"
                    idString="table-1"
                    columns={[{ name: "id", header: "Id"}, { name: "name", header: "Name" }]}
                    rows={props.pagedResult.items.map(x => x)}
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
          }
        </>
    );
}

export default PaginatedTable;
