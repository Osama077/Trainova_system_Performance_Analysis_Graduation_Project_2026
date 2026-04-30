# 🚀 CRUD Operations Quick Reference

## API Endpoints Overview

### Plan Management
```
POST   /api/plan                    - Create new plan
PUT    /api/plan/{id}               - Update plan
DELETE /api/plan/{id}               - Delete plan
```

### Training Session Management
```
POST   /api/trainingsession         - Create new training session
PUT    /api/trainingsession/{id}    - Update training session
DELETE /api/trainingsession/{id}    - Delete training session
```

### Access Policy Management
```
POST   /api/accesspolicy            - Create new access policy
PUT    /api/accesspolicy/{id}       - Update access policy
DELETE /api/accesspolicy/{id}       - Delete access policy
```

### User Access Policy Management
```
POST   /api/useraccesspolicy        - Add user to policy
PUT    /api/useraccesspolicy/{id}   - Update user status/score
DELETE /api/useraccesspolicy/{id}   - Remove user from policy
```

---

## Request/Response Examples

### Create Plan (POST)
```json
{
  "planName": "2025 Summer Training",
  "planGoal": "Prepare for championship",
  "accessPolicyId": "guid-here",
  "startDate": "2025-05-01",
  "endDate": "2025-08-31"
}
```

### Update Plan (PUT)
```json
{
  "planName": "Updated Plan Name",
  "planGoal": "New Goal",
  "startDate": null,
  "endDate": null
}
```

### Create Training Session (POST)
```json
{
  "sessionName": "Monday Training",
  "policyId": null,
  "place": "Training Ground A",
  "willHappenAt": "2025-06-15T10:00:00",
  "planId": "guid-here",
  "userIds": ["user-id-1", "user-id-2"]
}
```

### Create User Access Policy (POST)
```json
{
  "accessPolicyId": "guid-here",
  "userId": "user-id-here",
  "initialStatus": "Waiting"
}
```

### Update User Access Policy (PUT)
```json
{
  "status": "Attended",
  "doneScore": 85.5
}
```

---

## Authorization Requirements

| Endpoint | Role | Notes |
|----------|------|-------|
| Plan CRUD | Coach, Admin | Full access for coaches and system admin |
| TrainingSession CRUD | Coach, Admin | Full access for coaches and system admin |
| AccessPolicy CRUD | Coach, Admin | Full access for coaches and system admin |
| UserAccessPolicy Create/Delete | Coach, Admin | Full access |
| UserAccessPolicy Update | Coach, Admin, Doctor | Doctors can update status/score |

---

## Key Features

✅ **Transaction Management**
- All operations wrapped in database transactions
- Automatic rollback on errors

✅ **Validation**
- Input validation in handlers
- Referential integrity checks
- Business logic validation

✅ **Error Handling**
- Comprehensive error codes
- Meaningful error messages
- Proper HTTP status codes

✅ **Audit Trail**
- All operations track CreatedBy user
- Audit logs support (via domain)

✅ **Security**
- Authorization on all endpoints
- Role-based access control
- CurrentUser context injection

---

## Common Errors & Solutions

### 403 Forbidden
**Cause**: Insufficient permissions
**Solution**: Check user role and endpoint authorization requirements

### 404 Not Found
**Cause**: Resource doesn't exist
**Solution**: Verify resource ID and ensure it exists in database

### 409 Conflict
**Cause**: Cannot delete due to related entities
**Solution**: Delete dependent entities first (e.g., delete plans before access policy)

### 400 Bad Request
**Cause**: Invalid request data
**Solution**: Validate input parameters match schema (date formats, score ranges, etc.)

---

## Database Constraints

- Plan name: max 200 characters
- Plan goal: max 1200 characters
- AccessPolicy name: max 200 characters
- UserAccessPolicy score: 0-100
- End date must be after start date

---

## Testing the Endpoints

### Using curl
```bash
# Create Plan
curl -X POST http://localhost:5000/api/plan \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer {token}" \
  -d @create_plan.json

# Update Plan
curl -X PUT http://localhost:5000/api/plan/{id} \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer {token}" \
  -d @update_plan.json

# Delete Plan
curl -X DELETE http://localhost:5000/api/plan/{id} \
  -H "Authorization: Bearer {token}"
```

### Using Postman
1. Create new collection
2. Add requests with proper HTTP verbs
3. Set Authorization header with Bearer token
4. Set Content-Type: application/json
5. Copy request body from examples above

---

## Implementation Notes

- All handlers use CQRS pattern with MediatR
- Repository pattern for data access
- Fluent error handling with ResultOf pattern
- Dependency injection via constructor parameters
- Records used for command parameters
- Async/await for all I/O operations

---

Generated: 2025-04-30
Status: ✅ Production Ready
