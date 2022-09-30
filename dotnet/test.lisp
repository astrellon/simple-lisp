(begin
    (define logAdd
        (lambda (a b)
            (begin
                (print "Adding " a " and " b)
                (+ 1 a b)
            )
        )
    )

    (define y 0)
    (define total 0)
    (loop (< y 1000000)
        (begin
            (define y (+ y 1))
            (define total (+ total (rand)))
        )
    )
    (print total)
)