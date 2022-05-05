import { Pagination, PaginationItem, PaginationLink } from "reactstrap";

export default function Paging({
  numberOfRooms,
  from,
  to,
  pageSize,
  pageNumber,
  setPageNumber,
  sortOrder,
  sortBy,
}) {
  let totalPages = numberOfRooms / pageSize;
  let items = [];

  for (let pageNumber = 1; pageNumber < totalPages + 1; pageNumber++) {
    items.push(
      <button
        key={pageNumber}
        id={`page${pageNumber}`}
        style={{ backgroundColor: "white" }}
        onClick={() => {
          setPageNumber(pageNumber);
        }}
      >
        {pageNumber}
      </button>

      /*<PaginationItem key={pageNumber}>
        <PaginationLink
          id={`page${pageNumber}`}
          style={{ backgroundColor: "white" }}
          href={`/rooms/startDate=${from}-endDate=${to}-pageSize=${pageSize}-pageNumber=${pageNumber}-sortOrder=${sortOrder}-sortBy=${sortBy}`}
        >
          {pageNumber}
        </PaginationLink>
      </PaginationItem>*/
    );
  }

  return (
    <div style={{ display: "block", width: 700, padding: 30 }}>
      <Pagination>{items}</Pagination>
    </div>
  );
}
