export const dynamic = 'force-dynamic';

import React from 'react'
import AuctionForm from '../../AuctionForm';
import Heading from '@/app/components/Heading';
import { getDetailsViewData } from '@/app/actions/AuctionActions';

type UpdateProps = {
  params: Promise<{ id: string }>;
};

export default async function Update({ params }: UpdateProps) {
  const { id } = await params;
  const data = await getDetailsViewData(id);

  return (
    <div className='mx-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg'>
      <Heading title='Update your auction' subtitle='Please update the details of your car' />
      <AuctionForm auction={data} />
    </div>
  )
}