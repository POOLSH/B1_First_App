CREATE OR REPLACE FUNCTION calculate_sum_and_median()
RETURNS TABLE(sum_of_integers bigint, median_of_doubles numeric) AS $$
BEGIN
    -- Считаем сумму всех целых чисел
    SELECT COALESCE(SUM(rinteger), 0) INTO sum_of_integers FROM random;

    -- Считаем медиану всех дробных чисел
    WITH cte AS (
        SELECT rdouble,
               ROW_NUMBER() OVER (ORDER BY rdouble) AS row_num,
               COUNT(*) OVER () AS total_count
        FROM random
    )
    SELECT
        CASE
            WHEN total_count % 2 = 1 THEN rdouble
            ELSE AVG(rdouble) OVER (PARTITION BY total_count / 2) 
        END INTO median_of_doubles
    FROM cte
    WHERE row_num = total_count / 2 OR row_num = total_count / 2 + 1;

    -- Возвращаем результаты
    RETURN QUERY SELECT sum_of_integers, median_of_doubles;
END;
$$ LANGUAGE plpgsql;