import { Pagination, PaginationItem, PaginationLink } from "reactstrap";

export default function Paging({
  numberOfGuests,
  pageSize,
  pageNumber,
  sortOrder,
  sortBy,
}) {
  let totalPages = numberOfGuests / pageSize;
  let items = [];

  for (let pageNumber = 1; pageNumber < totalPages + 1; pageNumber++) {
    items.push(
      <PaginationItem id={`page${pageNumber}`}>
        <PaginationLink
          href={`/guestarchive/pageSize=${pageSize}-pageNumber=${pageNumber}-sortOrder=${sortOrder}-sortBy=${sortBy}`}
        >
          {pageNumber}
        </PaginationLink>
      </PaginationItem>
    );
  }

  return (
    <div style={{ display: "block", width: 700, padding: 30 }}>
      <Pagination>{items}</Pagination>
    </div>
  );
}
