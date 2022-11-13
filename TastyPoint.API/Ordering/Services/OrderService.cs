﻿using TastyPoint.API.Ordering.Domain.Models;
using TastyPoint.API.Ordering.Domain.Repositories;
using TastyPoint.API.Ordering.Domain.Services;
using TastyPoint.API.Ordering.Domain.Services.Communication;
using TastyPoint.API.Shared.Domain.Repositories;

namespace TastyPoint.API.Ordering.Services;

public class OrderService: IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Order>> ListAsync()
    {
        return await _orderRepository.ListAsync();
    }

    public async Task<OrderResponse> FindByIdAsync(int orderId)
    {
        var existingOrder = await _orderRepository.FindByIdAsync(orderId);
        
        if (existingOrder == null)
            return new OrderResponse("Pack not found");

        try
        {
            await _unitOfWork.CompleteAsync();
            return new OrderResponse(existingOrder);
        }
        catch (Exception e)
        {
            return new OrderResponse($"An error occurred while saving the category: {e.Message}");
        }
    }

    public async Task<OrderResponse> SaveAsync(Order order)
    {
        try
        {
            await _orderRepository.AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return new OrderResponse(order);
        }
        catch (Exception e)
        {
            return new OrderResponse($"An error occurred while saving the category: {e.Message}");
        }
    }

    public async Task<OrderResponse> UpdateAsync(int orderId, Order order)
    {
        var existingOrder = await _orderRepository.FindByIdAsync(orderId);

        if (existingOrder == null)
            return new OrderResponse("Pack not found");

        existingOrder.Restaurant = order.Restaurant;
        existingOrder.Status = order.Status;
        existingOrder.DeliveryMethod = order.DeliveryMethod;
        existingOrder.PaymentMethod = order.PaymentMethod;

        try
        {
            _orderRepository.Update(existingOrder);
            await _unitOfWork.CompleteAsync();

            return new OrderResponse(existingOrder);
        }
        catch (Exception e)
        {
            return new OrderResponse($"An error occurred while saving the category: {e.Message}");
        }
    }

    public async Task<OrderResponse> DeleteAsync(int id)
    {
        var existingOrder = await _orderRepository.FindByIdAsync(id);
        if (existingOrder == null)
            return new OrderResponse("Pack not found");

        try
        {
            _orderRepository.Remove(existingOrder);
            await _unitOfWork.CompleteAsync();

            return new OrderResponse(existingOrder);
        }
        catch (Exception e)
        {
            return new OrderResponse($"An error occurred while saving the category: {e.Message}");
        }
    }
}