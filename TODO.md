# TODO: Standardize SweetAlert for Delete Actions

## Overview
Update all MVC Index views to use consistent SweetAlert configuration for delete actions, matching the provided example.

## Files to Update
- [x] Views/EmployeesMVC/Index.cshtml
- [ ] Views/UsersMVC/Index.cshtml
- [ ] Views/DepartmentsMVC/Index.cshtml
- [ ] Views/ProjectsMVC/Index.cshtml
- [ ] Views/ReportsMVC/Index.cshtml

## Changes Required
- Swap confirm and cancel button colors to match example
- Add cancelButtonText: 'No, cancel!'
- Add handling for cancel dismissal with 'Cancelled' message
- Remove position: 'top' to match example exactly
- Ensure consistent Swal.fire options across all files

## Verification
- [x] Test delete confirmations work correctly
- [x] Test cancel actions show appropriate message
- [x] Ensure no breaking changes to existing functionality
