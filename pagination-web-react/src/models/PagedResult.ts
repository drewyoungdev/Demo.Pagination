
export type PagedResult = {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    pageCount: number;
    items: Test[]
}

type Test = {
    id: number;
    name: string;
}
