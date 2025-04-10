'use client'


import { deleteAuction } from '@/app/actions/AuctionActions'
import { Button, Spinner } from 'flowbite-react'
import { useRouter } from 'next/navigation'
import React, { useState } from 'react'
import toast from 'react-hot-toast'

type Props = {
    id: string
}

export default function DeleteButton({id}: Props) {
    const [loading, setLoading] = useState(false);
    const router = useRouter();
    
    function doDelete() {
        setLoading(true);
        deleteAuction(id)
            .then(res => {
                if (res.error) throw res.error;
                router.push('/');
            }).catch((error: any) => {
                toast.error(error.status + ' ' + error.message)
            }).finally(() => setLoading(false));
    }

    return (
        <Button color='failure' className='bg-red-600 cursor-pointer hover:bg-red-700 text-white' onClick={doDelete}>
            <Spinner size="sm" hidden={!loading} aria-label="Loading" className="me-3" light />
            Delete Auction
        </Button>
    )
}