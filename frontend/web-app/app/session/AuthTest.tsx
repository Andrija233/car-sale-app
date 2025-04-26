'use client'

import React, { useState } from 'react'
import { updateAuctionTest } from '../actions/AuctionActions';
import { Button } from 'flowbite-react';

export default function AuthTest() {
    const [loading, setLoading] = useState(false);
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const [result, setResult] = useState<any>();

    function doUpdate()
    {
        setResult(undefined);
        setLoading(true);
        updateAuctionTest()
            .then(res => setResult(res))
            .catch(err => setResult(err))
            .finally(() => setLoading(false));
    }
  return (
    <div className='flex items-center gap-4'>
        <Button outline  onClick={doUpdate} disabled={loading}>
            Test auth
        </Button>
        <div>
            {JSON.stringify(result, null, 2)}
        </div>
    </div>
  )
}
