CREATE FUNCTION sec.fn_securitypredicate(@username varchar(500))
    RETURNS TABLE
        WITH SCHEMABINDING
        AS
        RETURN SELECT 1 AS fn_securitypredicate_result
               WHERE (SESSION_CONTEXT(N'User') = @username);
GO  