import React, { useState, useEffect } from 'react';
import axios from 'axios';
import PaginatedTable from './components/PaginatedTable';
import { PagedResult } from './models/PagedResult';
import './App.scss';

const App: React.FC = () => {
  const [currentPageNumber, setCurrentPageNumber] = useState<number>(1);
  const [pagedResult, setPagedResult] = useState<PagedResult>();

  // useEffect will re-run whenever currentPageNumber is updated
  useEffect(() => {
    fetchData(currentPageNumber);
  }, [currentPageNumber]);

  const fetchData = async (currentPageNumber: number) => {
    const response = await axios
      .get<PagedResult>(`http://localhost:5000/api/values/paginated?currentPageNumber=${currentPageNumber}`)

    setPagedResult(response.data);
  }

  const onChange = (newPageNumber: number) => {
    setCurrentPageNumber(newPageNumber);
  };

  return (
    <div className="container">
      <PaginatedTable
        pagedResult={pagedResult}
        onChange={onChange}
      >
      </PaginatedTable>
    </div>
  );
}

export default App;
