CREATE SECURITY POLICY sec.TenantAccessPolicy
    ADD FILTER PREDICATE dbo.fn_securitypredicate(username) ON dbo.Product,
    ADD BLOCK PREDICATE dbo.TenantAccessPredicate(TenantId) ON dbo.Product
GO